'use strict';
import Menu from './components/Menu.js'

window.addEventListener("DOMContentLoaded", async () => {
    render();


});



function render(){
    
    const page = document.getElementById('page');
    const menu = Menu();

    const bg = document.createElement('img');
    bg.id = 'bg'
    bg.src = './icons/bg.svg'


    page.append(menu);
    page.append(bg);
}


