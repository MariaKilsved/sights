'use strict';
import Menu from './components/Menu.js'
import Card from './components/Card.js';

window.addEventListener("DOMContentLoaded", async () => {
    render();
});

function render(){
    
    const page = document.getElementById('page');
    const hello = document.createElement('p');
    hello.innerHTML = 'hello';
    const menu = Menu();
    const card = Card();
    page.append(card);
    page.append(menu);
    page.append(hello);


}



