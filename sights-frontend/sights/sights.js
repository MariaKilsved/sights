'use strict';
import Menu from '../components/Menu.js';

window.addEventListener("DOMContentLoaded", async () => {
    render();

    
});


function render(){
    
    const page = document.getElementById('page');
    const menu = Menu();
   

    const name = document.getElementById('p');
  



    page.append(name);
    page.append(card);

 
    

}