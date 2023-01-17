'use strict';

window.addEventListener("DOMContentLoaded", async () => {
    render();
});

function render(){
    const page = document.getElementById('page');
    const hello = document.createElement('p');
    hello.innerHTML = 'hello';
    page.append(hello);
}