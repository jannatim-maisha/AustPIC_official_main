/**
 * Define a function to navigate betweens form steps.
 * It accepts one parameter. That is - step number.
 */

var model;
const navigateToFormStep = (stepNumber) => {
    /**
     * Hide all form steps.
     */
	 //return;
    document.querySelectorAll(".form-step").forEach((formStepElement) => {
        formStepElement.classList.add("d-none");
    });
    /**
     * Mark all form steps as unfinished.
     */
    document.querySelectorAll(".form-stepper-list").forEach((formStepHeader) => {
        formStepHeader.classList.add("form-stepper-unfinished");
        formStepHeader.classList.remove("form-stepper-active", "form-stepper-completed");
    });
    /**
     * Show the current form step (as passed to the function).
     */
    document.querySelector("#step-" + stepNumber).classList.remove("d-none");
    /**
     * Select the form step circle (progress bar).
     */
    const formStepCircle = document.querySelector('li[step="' + stepNumber + '"]');
    /**
     * Mark the current form step as active.
     */
    formStepCircle.classList.remove("form-stepper-unfinished", "form-stepper-completed");
    formStepCircle.classList.add("form-stepper-active");
    /**
     * Loop through each form step circles.
     * This loop will continue up to the current step number.
     * Example: If the current step is 3,
     * then the loop will perform operations for step 1 and 2.
     */
    for (let index = 0; index < stepNumber; index++) {
        /**
         * Select the form step circle (progress bar).
         */
        const formStepCircle = document.querySelector('li[step="' + index + '"]');
        /**
         * Check if the element exist. If yes, then proceed.
         */
        if (formStepCircle) {
            /**
             * Mark the form step as completed.
             */
            formStepCircle.classList.remove("form-stepper-unfinished", "form-stepper-active");
            formStepCircle.classList.add("form-stepper-completed");
        }
    }
};
/**
 * Select all form navigation buttons, and loop through them.
 */
/*document.querySelectorAll(".btn-navigate-form-step").forEach((formNavigationBtn) => {
    /**
     * Add a click event listener to the button.
     
    formNavigationBtn.addEventListener("click", () => {
        /**
         * Get the value of the step.
         
        const stepNumber = parseInt(formNavigationBtn.getAttribute("step_number"));
        /**
         * Call the function to navigate to the target form step.
         
        navigateToFormStep(stepNumber);
    });
});*/

function firstNext()
{
	if(firstStepValidation()==true)
	{
		navigateToFormStep(2);
	}
};
function secondNext()
{
    if (firstStepValidation() == false) {
        navigateToFormStep(1);
    }
	else if(secondStepValidation()==true)
	{
		navigateToFormStep(3);
	}
};
function thirdPrev()
{
	navigateToFormStep(2);
};
function secondPrev()
{
	navigateToFormStep(1);
};
function validateFileType(){
    var fileName = document.getElementById("picture").value;
    if (fileName.length == 0) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Picture is empty!"
        });
        document.getElementById("picture").value = "";
        return false;
    }
    var idxDot = fileName.lastIndexOf(".") + 1;
    var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();
    if (extFile=="jpg" || extFile=="jpeg" || extFile=="png"){
        return true;
    }
    else
    {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Only jpg/jpeg and png files are allowed!"
        });
        document.getElementById("picture").value = "";
        return false;
    }   
};

function firstStepValidation() {
    //return true;
	///^([a-zA-Z]+\s)*[a-zA-Z]+$/
    let name = document.getElementById("name").value;
    let email = document.getElementById("email").value;
    let phone = document.getElementById("phone_number").value;
	if(nameValidation(name) && emailValidation(email) && numberValidation(phone))
        return true;
    return false;
	
};
function secondStepValidation() {
    //return true;
    let studentId  = document.getElementById("student_id").value;
    if(studentIdValidation(studentId)) return true;
	return false;
};
function thirdStepValidation() {
    //return true;
    let birthDate = document.getElementById("birth-date").value;
    return birthDateValidation(birthDate) && validateFileType();
};
function nameValidation(name)
{
    let pattern = /^([a-zA-Z]+\s)*[a-zA-Z]+$/;
    if (name.match(pattern)) return true;
    else if (name.length == 0) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Name is empty"
        });
    }
    else {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Name accepts only words and space between them"
        });
        return false;
    }
}
function emailValidation(email)
{
    const tester = /^[-!#$%&'*+\/0-9=?A-Z^_a-z`{|}~](\.?[-!#$%&'*+\/0-9=?A-Z^_a-z`{|}~])*@[a-zA-Z0-9](-*\.?[a-zA-Z0-9])*\.[a-zA-Z](-?[a-zA-Z0-9])+$/;
    if (!email) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Email is empty"
        });
        return false;
    }
    const emailParts = email.split('@');
    if (emailParts.length !== 2) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Not a valid email address"
        });
        return false;
    }
    const account = emailParts[0];
    const address = emailParts[1];
    if (account.length > 64) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Not a valid email address"
        });
        return false;
    }
    else if (address.length > 255) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Not a valid email address"
        });
        return false;
    }
    const domainParts = address.split('.');
    if (domainParts.some(function (part) {
        return part.length > 63;
    })) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Not a valid email address"
        });
        return false;
    }
    if (!tester.test(email)) return false;
        return true;
};
function numberValidation(number)
{
    const pattern = /^01[3-9]\d{8}$/;
    if (number.match(pattern))
        return true;
    else if (number.length == 0) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Mobile Number is Empty"
        });
    }
    else if (number.length != 11) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Number must contain 11 digits"
        });
    }
    else {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Number not valid"
        });
    }
    return false;
};
function studentIdValidation(Id)
{
    const pattern = /^(\d{9}|\d{11})$/;
    if(Id.match(pattern))
        return true;
    else if (Id.length == 0) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Student Id is Empty"
        });
        return false;
    }
    bootbox.alert({
        title: "<b>Error</b>",
        message: "Not a valid Student Id"
    });
    return false;
};
function birthDateValidation(dateString) {
    if (dateString.length == 0) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Birth Date is Empty"
        });
        return false;
    }
    const pattern = /^\d{4}\-\d{1,2}\-\d{1,2}$/;
    if (!dateString.match(pattern)) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Birth Date is not valid"
        });
        return false;
    }
    var parts = dateString.split("-");
    var day = parseInt(parts[2], 10);
    var month = parseInt(parts[1], 10);
    var year = parseInt(parts[0], 10);

    if (year < 1985 || year > 2020 || month == 0 || month > 12) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Birth Date is out of range"
        });
        return false;
    }

    var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
        monthLength[1] = 29;

    if ((day > 0 && day <= monthLength[month - 1])==false) {
        bootbox.alert({
            title: "<b>Error</b>",
            message: "Birth Date is not valid"
        });
        return false;
    }
    return true;
    
};

function finalSubmit() {

    if (!firstStepValidation() || !secondStepValidation() || !thirdStepValidation())
        return false;
    model = new FormData($('#userAccountSetupForm').get(0));
    let verificationDialog = bootbox.dialog({
        message: '<p class="text-center mb-0"><i class="fa fa-spin fa-cog"></i> Verifying Your Information...</p>',
        closeButton: false
    })
    .on('shown.bs.modal', function () {
        $.ajax({
            url: ClubWebsiteUrls.CHECK_NEW_MEMBER_INFO,
            type: "POST",
            cache: false,
            contentType: false,
            processData: false,
            data: model,

            success: function (response) {
                verificationDialog.modal('hide');
                if (response.status) {
                    window.location.href = ClubWebsiteUrls.PAYMENT_PAGE;
                }
                else if (!response.status) {
                    bootbox.alert({
                        title: "<b>Error</b>",
                        message: response.message
                    });
                }
                else {
                    bootbox.alert({
                        title: "<b>Error</b>",
                        message: "An Unknown Error Occured"
                    });
                }
            },
            error: function (err) {
                verificationDialog.modal('hide');
                bootbox.alert({
                    title: "<b>Error</b>",
                    message: "An Unknown Error Occured"
                });
            }
        });
    });
};



(function() {
  "use strict";

  /**
   * Easy selector helper function
   */
  const select = (el, all = false) => {
    el = el.trim()
    if (all) {
      return [...document.querySelectorAll(el)]
    } else {
      return document.querySelector(el)
    }
  }

  /**
   * Easy event listener function
   */
  const on = (type, el, listener, all = false) => {
    if (all) {
      select(el, all).forEach(e => e.addEventListener(type, listener))
    } else {
      select(el, all).addEventListener(type, listener)
    }
  }

  /**
   * Easy on scroll event listener 
   */
  const onscroll = (el, listener) => {
    el.addEventListener('scroll', listener)
  }

  /**
   * Navbar links active state on scroll
   */
  let navbarlinks = select('#navbar .scrollto', true)
  const navbarlinksActive = () => {
    let position = window.scrollY + 200
    navbarlinks.forEach(navbarlink => {
      if (!navbarlink.hash) return
      let section = select(navbarlink.hash)
      if (!section) return
      if (position >= section.offsetTop && position <= (section.offsetTop + section.offsetHeight)) {
        navbarlink.classList.add('active')
      } else {
        navbarlink.classList.remove('active')
      }
    })
  }
  window.addEventListener('load', navbarlinksActive)
  onscroll(document, navbarlinksActive)

  /**
   * Scrolls to an element with header offset
   */
  const scrollto = (el) => {
    let header = select('#header')
    let offset = header.offsetHeight

    if (!header.classList.contains('header-scrolled')) {
      offset -= 10
    }

    let elementPos = select(el).offsetTop
    window.scrollTo({
      top: elementPos - offset,
      behavior: 'smooth'
    })
  }

  /**
   * Toggle .header-scrolled class to #header when page is scrolled
   */
  let selectHeader = select('#header')
  if (selectHeader) {
    const headerScrolled = () => {
      if (window.scrollY > 100) {
        selectHeader.classList.add('header-scrolled')
      } else {
        selectHeader.classList.remove('header-scrolled')
      }
    }
    window.addEventListener('load', headerScrolled)
    onscroll(document, headerScrolled)
  }

  /**
   * Back to top button
   */
  let backtotop = select('.back-to-top')
  if (backtotop) {
    const toggleBacktotop = () => {
      if (window.scrollY > 100) {
        backtotop.classList.add('active')
      } else {
        backtotop.classList.remove('active')
      }
    }
    window.addEventListener('load', toggleBacktotop)
    onscroll(document, toggleBacktotop)
  }

  /**
   * Mobile nav toggle
   */
  on('click', '.mobile-nav-toggle', function(e) {
    select('#navbar').classList.toggle('navbar-mobile')
    this.classList.toggle('bi-list')
    this.classList.toggle('bi-x')
  })

  /**
   * Mobile nav dropdowns activate
   */
  on('click', '.navbar .dropdown > a', function(e) {
    if (select('#navbar').classList.contains('navbar-mobile')) {
      e.preventDefault()
      this.nextElementSibling.classList.toggle('dropdown-active')
    }
  }, true)

  /**
   * Scrool with ofset on links with a class name .scrollto
   */
  on('click', '.scrollto', function(e) {
    if (select(this.hash)) {
      e.preventDefault()

      let navbar = select('#navbar')
      if (navbar.classList.contains('navbar-mobile')) {
        navbar.classList.remove('navbar-mobile')
        let navbarToggle = select('.mobile-nav-toggle')
        navbarToggle.classList.toggle('bi-list')
        navbarToggle.classList.toggle('bi-x')
      }
      scrollto(this.hash)
    }
  }, true)

  /**
   * Scroll with ofset on page load with hash links in the url
   */
  window.addEventListener('load', () => {
    if (window.location.hash) {
      if (select(window.location.hash)) {
        scrollto(window.location.hash)
      }
    }
  });

  /**
   * Clients Slider
   */
  new Swiper('.clients-slider', {
    speed: 400,
    loop: true,
    autoplay: {
      delay: 5000,
      disableOnInteraction: false
    },
    slidesPerView: 'auto',
    pagination: {
      el: '.swiper-pagination',
      type: 'bullets',
      clickable: true
    },
    breakpoints: {
      320: {
        slidesPerView: 2,
        spaceBetween: 40
      },
      480: {
        slidesPerView: 3,
        spaceBetween: 60
      },
      640: {
        slidesPerView: 4,
        spaceBetween: 80
      },
      992: {
        slidesPerView: 6,
        spaceBetween: 120
      }
    }
  });

  /**
   * Porfolio isotope and filter
   */
  window.addEventListener('load', () => {
    let portfolioContainer = select('.portfolio-container');
    if (portfolioContainer) {
      let portfolioIsotope = new Isotope(portfolioContainer, {
        itemSelector: '.portfolio-item',
        layoutMode: 'fitRows'
      });

      let portfolioFilters = select('#portfolio-flters li', true);

      on('click', '#portfolio-flters li', function(e) {
        e.preventDefault();
        portfolioFilters.forEach(function(el) {
          el.classList.remove('filter-active');
        });
        this.classList.add('filter-active');

        portfolioIsotope.arrange({
          filter: this.getAttribute('data-filter')
        });
        aos_init();
      }, true);
    }

  });

  /**
   * Initiate portfolio lightbox 
   */
  const portfolioLightbox = GLightbox({
    selector: '.portfokio-lightbox'
  });

  /**
   * Portfolio details slider
   */
  new Swiper('.portfolio-details-slider', {
    speed: 400,
    autoplay: {
      delay: 5000,
      disableOnInteraction: false
    },
    pagination: {
      el: '.swiper-pagination',
      type: 'bullets',
      clickable: true
    }
  });

  /**
   * Testimonials slider
   */
  new Swiper('.testimonials-slider', {
    speed: 600,
    loop: true,
    autoplay: {
      delay: 5000,
      disableOnInteraction: false
    },
    slidesPerView: 'auto',
    pagination: {
      el: '.swiper-pagination',
      type: 'bullets',
      clickable: true
    },
    breakpoints: {
      320: {
        slidesPerView: 1,
        spaceBetween: 40
      },

      1200: {
        slidesPerView: 3,
      }
    }
  });

  /**
   * Animation on scroll
   */
  function aos_init() {
    AOS.init({
      duration: 1000,
      easing: "ease-in-out",
      once: true,
      mirror: false
    });
  }
  window.addEventListener('load', () => {
    aos_init();
  });

  /**
   * Initiate Pure Counter 
   */
  new PureCounter();

})();

