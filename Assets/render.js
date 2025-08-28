const DOCUMENT_PATH = 'templates\\document.html';
const PAPER_PATH = 'templates\\paper.html';
const CARD_PATH = 'templates\\card.html';
const DATA_PATH = 'data\\example_data.json';

const CARDS_BY_PAGE = 10;

async function renderDocument(cardCount) {
    let document = await loadDocument();
    let papers = '';
    const paperTemplate = await loadPaper();
    let papersCount = Math.floor(cardCount / CARDS_BY_PAGE);
    for (let i = 0; i < papersCount; i++) {
        papers = await addCardsToPaper(papers, paperTemplate, CARDS_BY_PAGE);
    }
    const remain = cardCount % CARDS_BY_PAGE;
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
    const data = await getData();
    const template = await loadCard();
    for (let i = 0; i < cardCount; i++) {
        let card = template;
        for (const key in data) {
            card = card.replace(`{${key}}`, data[key]);
        }
        renderedCards[i] = card;
    }
    return renderedCards;
}

async function getData() {
    const data = await fetch(DATA_PATH);
    return await data.json();
}

async function loadCard() {
    const response = await fetch(CARD_PATH);
    return await response.text();
}