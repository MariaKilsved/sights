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


function render(location,sights){

  
    const page = document.getElementById('page');
    const menu = Menu();
    const name = document.createElement('p');
    name.innerHTML=location;

    
    sights.forEach((attraction) => {
        const row = document.createElement('tr')
        
        const title = document.createElement('td');
        const link = document.createElement('a');
        link.className = 'link';
        link.innerHTML = attraction.title;
        link.href = `/comment/?id=${attraction.id}`;
        
        title.append(link);
        row.append(title);
        sights.append(row);
    });

  

    page.append(name);
    page.append(menu);
    

 
    

}