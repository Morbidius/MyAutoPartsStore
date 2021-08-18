// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
	//Check to see if the window is top if not then display button
	$(window).scroll(function () {
		if ($(this).scrollTop() > 100) {
			$('.scrollToTop').fadeIn();
		} else {
			$('.scrollToTop').fadeOut();
		}
	});
	//Click event to scroll to top
	$('.scrollToTop').click(function () {
		$('html, body').animate({ scrollTop: 0 }, 800);
		return false;
	});
});

$(document).ready(function () {

	window.setTimeout(function () {
		$(".banner").fadeTo(1000, 0).slideUp(1000, function () {
			$(this).remove();
		});
	}, 5000);

});


document.querySelectorAll('.buy-btn.btn-danger').forEach(btn => {
	btn.addEventListener('click', e => {
		e.preventDefault();
		let productId = Number(e.currentTarget.getAttribute('data-key'));

		const requestUrl = `${window.location.origin}/Order/AddToCart`;

		$.ajax({
			type: "POST",
			url: requestUrl,
			data: {
				productId
			},
			contentType: 'application/x-www-form-urlencoded',
			success: function (result) {
				if (result.error == false) {
					document.getElementById('product-count').innerText = result.productCount;
				} else if (result.error == true) {
					console.log(result);
				}
			},
			error: function (err) {
				console.log(err.status);
			}
		});
	})
})

