const DOCUMENT_PATH = 'templates\\document.html';
const PAPER_PATH = 'templates\\paper.html';
const CARD_PATH = 'templates\\card.html';

async function renderDocument(cardCount) {
    let document = await loadDocument();
    let papers = '';
    const paperTemplate = await loadPaper();
    let papersCount = Math.floor(cardCount / 8);
    for (let i = 0; i < papersCount; i++) {
        papers = await addCardsToPaper(papers, paperTemplate, 8);
    }
    const remain = cardCount % 8;
    if (remain > 0)  {
        papers = await addCardsToPaper(papers, paperTemplate, remain);
    }
    document = document.replace('{papers}', papers);
    document = document.replace('{title}', 'Пример с билетами: ' + cardCount);
    return document;
}

async function addCardsToPaper(papers, paperTemplate, cardCount) {
    const cards = await getCards(cardCount);
    let paper = paperTemplate.replace('{cards}', ''.concat(cards.join('')));
    papers = papers.concat(paper);
    return papers;
}

async function loadDocument() {
    const response = await fetch(DOCUMENT_PATH);
    return await response.text();
}

async function loadPaper() {
    const response = await fetch(PAPER_PATH);
    return await response.text();
}

async function getCards(cardCount) {
    const renderedCards = new Array(cardCount);
    const template = await loadCard();
    for (let i = 0; i < cardCount; i++) {
        let card = template.replace('{codbar}', 1234567890);
        card = card.replace('{lastname}', 'Фамилия');
        card = card.replace('{givenNames}', 'Имя Отчество');
        card = card.replace('{faculty}', 'ФКЛТТ');
        card = card.replace('{group}', '10203040');
        card = card.replace('{department}', 'Студент ДО');
        card = card.replace('{registrationDate}', '2025-09-01');
        card = card.replace('{graduationDate}', '2030-09-01');
        card = card.replace('{portraitPath}', 'portrait.png');
        card = card.replace('{barcodePath}', 'barcode.png');
        renderedCards[i] = card;
    }
    return renderedCards;
}

async function loadCard() {
    const response = await fetch(CARD_PATH);
    return await response.text();
}