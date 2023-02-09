'use strict';
import Menu from '../components/Menu.js';
import Card from '../components/Card.js';
import {get} from '../lib/request.js';

window.addEventListener("DOMContentLoaded", async () => {
    const queryString = window.location.search;
    const location = new URLSearchParams(queryString).get('location')

    const sights = await get('https://localhost:7260/api/Attraction/ByLikes');
    const countries = await fetch('https://localhost:7260/api/Country').then(res => res.json());
    const cities = await fetch('https://localhost:7260/api/City').then(res => res.json());
    
    render(location,sights,countries,cities);

});


function render(location, sights, countries, cities){
    
    const page = document.getElementById('page');
    const menu = Menu();

    const name = document.createElement('p');
    name.innerHTML=location;

    const card = Card();

    const searchTable = document.createElement('table');
    searchTable.id = 'search-table';
  
    let isCountry;
    let id;

    countries.forEach(country => {
        if(location === country.name){
            isCountry = true;
            id = country.id;
        } 
    });

    if(!isCountry){
        cities.forEach(city => {
            if(location === city.name){
                id = city.id;
            } 
        });
    }

    sights.forEach(sight => {
        if(isCountry){
            if(sight.attraction.countryId === id){
                const row = document.createElement('tr')
        
                const title = document.createElement('td');
                const likes = document.createElement('td');
                const link = document.createElement('a');
                link.className = 'link';
                link.href = `/comment/?id=${sight.attraction.id}`;

                const linkTitle = document.createElement('p');
                linkTitle.id='linkTitle';
                linkTitle.innerHTML=sight.attraction.title;
                
                const nrOfLikes = document.createElement('p');
                nrOfLikes.innerHTML = sight.likeCount + " likes";

                const description = document.createElement('p');
                description.id='description';
                description.innerHTML=sight.attraction.description;
                

              
                link.append(linkTitle, description);
                likes.append(nrOfLikes);
                title.append(link);
                row.append(title);
                row.append(likes);
                searchTable.append(row);
            }
        } else {
            if(sight.attraction.cityId === id){
                const row = document.createElement('tr')

                const title = document.createElement('td');
                const likes = document.createElement('td');
                const link = document.createElement('a');
                link.className = 'link';
                link.href = `/comment/?id=${sight.attraction.id}`;
                
                const linkTitle = document.createElement('p');
                linkTitle.id='linkTitle';
                linkTitle.innerHTML=sight.attraction.title;

                const nrOfLikes = document.createElement('p');
                nrOfLikes.innerHTML = sight.likeCount + " likes";

                const description = document.createElement('p');
                description.id='description';
                description.innerHTML=sight.attraction.description;
        
                link.append(linkTitle, description);
                likes.append(nrOfLikes);
                title.append(link);
                row.append(title);
                row.append(likes);
                searchTable.append(row);
            }
        }
    })

    page.append(menu);
    page.append(name);
    card.append(searchTable);
    page.append(card);
}