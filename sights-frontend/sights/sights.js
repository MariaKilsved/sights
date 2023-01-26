'use strict';
import Menu from '../components/Menu.js';

window.addEventListener("DOMContentLoaded", async () => {
    const queryString = window.location.search;
    const location = new URLSearchParams(queryString).get('location')

    const sights = await fetch('https://localhost:7260/api/Attraction').then(res => res.json());
    const countries = await fetch('https://localhost:7260/api/Country').then(res => res.json());
    const cities = await fetch('https://localhost:7260/api/City').then(res => res.json());
    
    console.log(location);
    render(location,sights,countries,cities);

});


function render(location){

  
    const page = document.getElementById('page');
    const menu = Menu();
    const name = document.createElement('p');
    name.innerHTML=location;

    


  

    page.append(name);
    page.append(menu);
    

 
    

}