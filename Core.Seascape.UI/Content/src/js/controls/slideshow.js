import Swiper from '../lib/swiper/swiper-bundle.esm.browser.js'

const Slideshow = {

    init() {
        new Swiper('.swiper-container', {
            loop: true,
            slideClass: 'swiper-slide--bg',
            navigation: {
                nextEl: '.swiper-button-next',
                prevEl: '.swiper-button-prev',
            },
            pagination: {
                el: '.swiper-pagination',
                dynamicBullets: true,
            }
        });
    }

}

export default Slideshow