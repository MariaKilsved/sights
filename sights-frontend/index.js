'use strict';
import Menu from './components/Menu.js'
import Card from './components/Card.js';

window.addEventListener("DOMContentLoaded", async () => {
    render();
});

function render(){
    const page = document.getElementById('page');
    
    const menu = Menu();
    const card = Card();
    const hello = document.createElement('p');
    hello.innerHTML = 'hello';
    card.append(hello)
    page.append(card);
    page.append(menu);

}



