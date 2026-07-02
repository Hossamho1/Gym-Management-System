/**
 * Power Fitness — home page polish (scroll, reveal, counters)
 */
(function () {
    'use strict';

    var prefersReducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;

    function markLoaded() {
        document.body.classList.add('gym-is-loaded');
    }

    function revealHero() {
        document.querySelectorAll('.gym-hero .gym-reveal, .gym-hero .gym-reveal-stagger').forEach(function (el) {
            el.classList.add('is-visible');
        });
    }

    function showAllReveals() {
        document.querySelectorAll('.gym-reveal, .gym-reveal-stagger').forEach(function (el) {
            el.classList.add('is-visible');
        });
    }

    /* —— Boot: always show page content —— */
    markLoaded();
    revealHero();

    /* —— Scroll progress —— */
    var progressBar = document.querySelector('.gym-scroll-progress__bar');
    function updateScrollProgress() {
        if (!progressBar) return;
        var scrollTop = window.scrollY || document.documentElement.scrollTop;
        var height = document.documentElement.scrollHeight - window.innerHeight;
        var pct = height > 0 ? Math.min(100, (scrollTop / height) * 100) : 0;
        progressBar.style.width = pct + '%';
    }

    /* —— Navbar shadow + back to top —— */
    var nav = document.getElementById('gymNav');
    function updateNav() {
        if (nav) {
            nav.classList.toggle('scrolled', window.scrollY > 24);
        }
        updateScrollProgress();
        var backTop = document.querySelector('.gym-back-top');
        if (backTop) {
            backTop.classList.toggle('is-visible', window.scrollY > 480);
        }
    }

    window.addEventListener('scroll', updateNav, { passive: true });
    updateNav();

    /* —— Back to top —— */
    var backTopBtn = document.querySelector('.gym-back-top');
    if (backTopBtn) {
        backTopBtn.addEventListener('click', function () {
            window.scrollTo({ top: 0, behavior: prefersReducedMotion ? 'auto' : 'smooth' });
        });
    }

    /* —— Smooth anchor scroll —— */
    document.querySelectorAll('a[href^="#"]').forEach(function (link) {
        link.addEventListener('click', function (e) {
            var id = link.getAttribute('href');
            if (!id || id === '#') return;
            var target = document.querySelector(id);
            if (!target) return;
            e.preventDefault();
            target.scrollIntoView({ behavior: prefersReducedMotion ? 'auto' : 'smooth', block: 'start' });
        });
    });

    /* —— Scroll reveal (skip hero — already visible) —— */
    if (prefersReducedMotion || !('IntersectionObserver' in window)) {
        showAllReveals();
    } else {
        var revealEls = document.querySelectorAll('.gym-reveal, .gym-reveal-stagger');
        var revealObserver = new IntersectionObserver(
            function (entries) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        entry.target.classList.add('is-visible');
                        revealObserver.unobserve(entry.target);
                    }
                });
            },
            { root: null, rootMargin: '0px 0px -5% 0px', threshold: 0.08 }
        );
        revealEls.forEach(function (el) {
            if (el.closest('.gym-hero')) return;
            revealObserver.observe(el);
        });
    }

    /* —— Stat counters —— */
    function animateCounter(el, target, duration) {
        var startTime = null;
        function step(timestamp) {
            if (!startTime) startTime = timestamp;
            var progress = Math.min((timestamp - startTime) / duration, 1);
            var eased = 1 - Math.pow(1 - progress, 3);
            el.textContent = String(Math.floor(target * eased));
            if (progress < 1) {
                requestAnimationFrame(step);
            } else {
                el.textContent = String(target);
            }
        }
        requestAnimationFrame(step);
    }

    var statValues = document.querySelectorAll('.gym-stat__value[data-count]');
    if (prefersReducedMotion || !('IntersectionObserver' in window)) {
        statValues.forEach(function (el) {
            var target = parseInt(el.getAttribute('data-count'), 10);
            if (!isNaN(target)) el.textContent = String(target);
        });
    } else {
        var counterObserver = new IntersectionObserver(
            function (entries) {
                entries.forEach(function (entry) {
                    if (!entry.isIntersecting) return;
                    var el = entry.target;
                    var target = parseInt(el.getAttribute('data-count'), 10);
                    if (isNaN(target)) return;
                    el.textContent = '0';
                    animateCounter(el, target, 1400);
                    counterObserver.unobserve(el);
                });
            },
            { threshold: 0.25 }
        );
        statValues.forEach(function (el) {
            counterObserver.observe(el);
        });
    }

    /* —— Subtle hero parallax —— */
    var parallaxEl = document.querySelector('[data-parallax]');
    if (parallaxEl && !prefersReducedMotion) {
        window.addEventListener(
            'scroll',
            function () {
                var y = window.scrollY;
                if (y > 800) return;
                parallaxEl.style.transform = 'translateY(' + y * 0.06 + 'px)';
            },
            { passive: true }
        );
    }

    /* —— Workout carousel dots —— */
    document.querySelectorAll('.gym-workouts__dot').forEach(function (dot, i, dots) {
        dot.addEventListener('click', function () {
            dots.forEach(function (d) {
                d.classList.remove('active');
            });
            dot.classList.add('active');
        });
    });
})();
