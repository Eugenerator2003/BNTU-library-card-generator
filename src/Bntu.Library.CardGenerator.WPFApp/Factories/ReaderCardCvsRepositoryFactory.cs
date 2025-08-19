using Bntu.Library.CardGenerator.Infrastructure.Tables.Repositories;
using Bntu.Library.CardGenerator.WPFApp.Factories.PathProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.WPFApp.Factories
{
    public class ReaderCardCvsRepositoryFactory : IFactory<ReaderCardCvsRepository>
    {
        public ReaderCardCvsRepository Create()
        {
            var pathProvider = CsvFilesPathProvider.Instance;
            if (string.IsNullOrEmpty(pathProvider.ReaderCardsSource))
                throw new ArgumentNullException("", "Не указан путь к csv-файлу с данными читательских билетов.");
            return new ReaderCardCvsRepository(pathProvider.ReaderCardsSource);
        }
    }
}
