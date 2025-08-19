using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Export;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import;
using Bntu.Library.CardGenerator.Application.StudentsImport.Models.Import.Aggregates;
using Bntu.Library.CardGenerator.Application.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Application.StudentsImport.Services
{
    public class InformationTransformationService
    {
        private GroupCollection _groups;
        private ProfessionCollection _professions;
        private SpecializationCollection _specializations;
        private FacultyCollection _facultets;

        public InformationTransformationService(ProfessionCollection professionCollection, SpecializationCollection specializationCollection)
        {
            _groups = new GroupCollection();
            _professions = professionCollection;
            _specializations = specializationCollection;
            _facultets = new FacultyCollection();
        }

        public ApplicationResult<IList<ImportInformation>> Transform(IList<ExportedInformation> exportedInformation, ImportingParameter parameter)
        {
            _groups.Reset();
            _facultets = new FacultyCollection();
            var list = new List<ImportInformation>();

            try
            {
                int codbar = parameter.StartCodbar;
                var libraryRegistration = GetLibraryRegistration(parameter.CurrentYear);
                for (int i = 0; i < exportedInformation.Count; i++)
                {
                    if (exportedInformation[i].StudyInformation.StudyStartDate.Year != parameter.CurrentYear.Year)
                        continue;

                    var student = GetStudentInformation(exportedInformation[i].Student);
                    var contact = GetContactInformation(exportedInformation[i].ContactInformation);
                    var studyGroup = GetStudyGroup(exportedInformation[i].StudyInformation, parameter);
                    var lineResult = new ImportInformation(codbar, exportedInformation[i].Photoname, student, contact,
                                                           studyGroup, libraryRegistration);
                    list.Add(lineResult);
                    codbar++;
                }

                return ApplicationResult<IList<ImportInformation>>.Success(list, "Данные успешно преобразованы");
            }
            catch (Exception ex)
            {
                return ApplicationResult<IList<ImportInformation>>.Error($"{ex.GetType()}\n{ex.Message}\n{ex.StackTrace}");
            }
        }

        private ImportStudentInformation GetStudentInformation(ExportedStudentInformation studentInformation)
        {
            var import = new ImportStudentInformation(studentInformation.Sex, studentInformation.Lastname,
                                                      $"{studentInformation.Firstname} {studentInformation.Patronymic}",
                                                      studentInformation.BirthDate,
                                                      $"{studentInformation.PassportNumber} {studentInformation.PassportInfo}");
            return import;
        }

        private ImportContactInformation GetContactInformation(ExportedContactInformation contactInformation)
        {
            var import = new ImportContactInformation(contactInformation.City, contactInformation.GetAddress(),
                                                      new List<string>(contactInformation.Phones), contactInformation.Email);
            return import;
        }

        private ImportStudyGroup GetStudyGroup(ExportedStudyInformation studyInformation, ImportingParameter parameter)
        {
            var group = _groups[studyInformation.GroupNumber];
            if (group is not null)
            {
                return group;
            }

            var faculty = GetFaculty(studyInformation.Faculty, parameter);
            var specialization = GetSpecialization(studyInformation.Specialization);
            var profession = GetProfession(specialization);
            var graduationDate = new DateTime(studyInformation.GraduationDate.Year, 9, 1);

            group = new ImportStudyGroup(faculty, profession, specialization, studyInformation.GroupNumber, 
                                         studyInformation.Department, graduationDate);
            _groups.Add(group);
            return group;
        }

        private Faculty GetFaculty(string facultyDefaultName, ImportingParameter parameter)
        {
            var facultyName = facultyDefaultName + "-" + (parameter.CurrentYear.Year % 2000).ToString();
            var faculty = _facultets[facultyName];
            if (faculty is null)
            {
                faculty = new Faculty(facultyName);
                _facultets.Add(faculty);
            }
            return faculty;
        }

        private Profession? GetProfession(Specialization? specialization)
        {
            if (specialization is not null && specialization.ProfessionId is not null)
                return _professions[specialization.ProfessionId.Value];
            else
                return null;
        }

        private Specialization? GetSpecialization(string semiformattedNumber)
        {
            return _specializations[semiformattedNumber];
        }

        private LibraryRegistration GetLibraryRegistration(DateTime currentYear)
        {
            return new LibraryRegistration(new DateTime(currentYear.Year, 9, 1));
        }
    }
}
