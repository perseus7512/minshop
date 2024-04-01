/* 頁數欄位 */
document.addEventListener("DOMContentLoaded", function() {
    const prevBtn = document.getElementById('prev');
    const nextBtn = document.getElementById('next');
    const pages = document.querySelectorAll('.page');
    let currentPage = 0;

    function showPage(pageIndex) {
        pages.forEach((page, index) => {
            if (index === pageIndex) {
                page.style.display = 'inline-block';
                page.style.backgroundColor = '#17A2B8';
                page.style.color = '#ffffff';
            } else {
                page.style.display = 'inline-block';
                page.style.backgroundColor = '#f0f0f0';
                page.style.color = '#333';
            }
        });
    }
    

    function goToPrevPage() {
        if (currentPage > 0) {
            currentPage--;
            showPage(currentPage);
        }
    }

    function goToNextPage() {
        if (currentPage < pages.length - 1) {
            currentPage++;
            showPage(currentPage);
        }
    }

    prevBtn.addEventListener('click', goToPrevPage);
    nextBtn.addEventListener('click', goToNextPage);

    // Show the first page initially with the selected style
    showPage(currentPage);
});
