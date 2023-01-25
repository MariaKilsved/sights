'use strict';
import Menu from './components/Menu.js';
import {get} from './lib/request.js';

window.addEventListener("DOMContentLoaded", async () => {

    const sights = await fetch('https://localhost:7260/api/Attraction').then(res => res.json());
    const countries = await fetch('https://localhost:7260/api/Country').then(res => res.json());
    const cities = await fetch('https://localhost:7260/api/City').then(res => res.json());
    console.log(sights, countries, cities)    
    render();

    const search = document.getElementById('search');
    const searchResults = document.getElementById('search-results');
    const attractionTable = document.createElement('table');
    attractionTable.id = 'attraction-table';

    sights.forEach((attraction) => {
        const row = document.createElement('tr')
        
        const title = document.createElement('td');
        title.innerHTML = attraction.title;
        
        const country = document.createElement('td');
        countries.forEach((c) => {
            if(attraction.countryId === c.id){
                country.innerHTML = c.name;
            }
        });
        
        const city = document.createElement('td');
        cities.forEach((c) => {
            if(attraction.cityId === c.id){
                city.innerHTML = c.name;
            }
        });
        
        row.append(title);
        row.append(country);
        row.append(city);
        
        attractionTable.append(row);
    });

    searchResults.append(attractionTable);

    search.addEventListener('input', () => {
        console.log(search.value)
    })

});

function myFunction() {
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("search");
    filter = input.value.toUpperCase();
    table = document.getElementById("attraction-table");
    tr = table.getElementsByTagName("tr");
    for (i = 0; i < tr.length; i++) {
      td = tr[i].getElementsByTagName("td")[0];
      if (td) {
        txtValue = td.textContent || td.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
          tr[i].style.display = "";
        } else {
          tr[i].style.display = "none";
        }
      }       
    }
  }

function render(){
    
    const page = document.getElementById('page');
    const menu = Menu();

    const search = document.createElement('input');
    search.type = 'text';
    search.id = 'search';

    const searchResults = document.createElement('div');
    searchResults.id = 'search-results';

    const bg = document.createElement('img');
    bg.id = 'bg';
    bg.src = './icons/bg.svg';

    page.append(menu);
    page.append(search);
    page.append(searchResults);
    page.append(bg);
}


