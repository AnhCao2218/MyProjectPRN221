// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const urlParams = new URLSearchParams(window.location.search);
const message = urlParams.get('message');
if (message) {
    document.getElementById('notification').innerHTML = message
    document.getElementById('notification').hidden = false
}

setTimeout(() => {
    document.getElementById('notification').hidden = true
}, 5000)
