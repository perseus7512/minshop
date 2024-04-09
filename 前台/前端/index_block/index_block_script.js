document.addEventListener("DOMContentLoaded", function() {
    const prevBtn = document.getElementById('prev');
    const nextBtn = document.getElementById('next');
    const productsInner = document.querySelector('.products-inner');
    const productsContainer = document.querySelector('.product-container');
    const productCards = document.querySelectorAll('.card');
    let visibleCards = calculateVisibleCards(); // 計算可視卡片數量
    let currentPage = 0;

    function calculateVisibleCards() {
        const containerWidth = productsContainer.offsetWidth - 40; // 減去左右內邊距
        const cardWidth = productCards[0].offsetWidth;
        const totalWidth = productCards.length * cardWidth;
        return Math.floor(containerWidth / cardWidth);
    }

    function goToPrevPage() {
        if (currentPage > 0) {
            currentPage--;
            scrollToPage(currentPage);
        }
    }

    function goToNextPage() {
        const totalPages = Math.ceil(productCards.length / visibleCards);
        if (currentPage < totalPages - 1) {
            currentPage++;
            scrollToPage(currentPage);
        }
    }

    function scrollToPage(pageIndex) {
        const cardWidth = productsContainer.offsetWidth / visibleCards;
        const scrollX = pageIndex * (cardWidth * visibleCards);
        productsInner.style.transform = `translateX(-${scrollX}px)`;
    }

    prevBtn.addEventListener('click', goToPrevPage);
    nextBtn.addEventListener('click', goToNextPage);

    // 調整視窗大小時重新計算可視卡片數量
    window.addEventListener('resize', function() {
        visibleCards = calculateVisibleCards();
        scrollToPage(currentPage);
    });

    // 調整商品卡片容器的寬度
    productsInner.style.width = `${productCards.length * 100}%`;
});