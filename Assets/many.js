document.addEventListener('DOMContentLoaded', async function () {
    document.documentElement.innerHTML = await renderDocument(19);
    
    document.querySelectorAll('script').forEach(s => {
        window.eval(s.textContent);
    });
});