document.addEventListener('DOMContentLoaded', async function () {
    document.documentElement.innerHTML = await renderDocument(1);
});