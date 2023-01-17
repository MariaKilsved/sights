'use strict';
import Menu from './components/Menu.js'

window.addEventListener("DOMContentLoaded", async () => {
    render();
});

function render(){
    
    const page = document.getElementById('page');
    const hello = document.createElement('p');
    hello.innerHTML = 'hello';
    const menu = Menu();
    page.append(menu);
    page.append(hello);

}



