'use strict';
import Menu from './components/Menu.js';
import {get} from './lib/request.js';

window.addEventListener("DOMContentLoaded", async () => {

    const sights = await fetch('https://localhost:7260/api/Attraction').then(res => res.json());
    const countries = await fetch('https://localhost:7260/api/Country').then(res => res.json());
    const cities = await fetch('https://localhost:7260/api/City').then(res => res.json());

    render();

    const searchResults = document.getElementById('search-results');
    const attractionTable = document.createElement('table');
    attractionTable.id = 'attraction-table';

    sights.forEach((attraction) => {
        const row = document.createElement('tr')
        
        const title = document.createElement('td');
        const link = document.createElement('a');
        link.className = 'link';
        link.innerHTML = attraction.title;
        link.href = `/comment/?id=${attraction.id}`;
        
        title.append(link);
        row.append(title);
        attractionTable.append(row);
    });
    countries.forEach((country) => {
      const row = document.createElement('tr')
        
      const name = document.createElement('td');
      const link = document.createElement('a');
      link.className = 'link';
      link.innerHTML = country.name;
      link.href = `/sights/?location=${country.name}`;

      name.append(link);
      row.append(name);
      attractionTable.append(row);
    })
    cities.forEach((city) => {
      const row = document.createElement('tr')
        
      const name = document.createElement('td');
      const link = document.createElement('a');
      link.className = 'link';
      link.innerHTML = city.name;
      link.href = `/sights/?location=${city.name}`;

      name.append(link);
      row.append(name);
      attractionTable.append(row);
    })

    searchResults.append(attractionTable);

    search.addEventListener('input', () => {
      var input, filter, table, tr, td, i, txtValue;
      input = document.getElementById("search");
      if(input.value !== ""){
         searchResults.style.display = ""
      } else {
        searchResults.style.display = "none"
      }
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
    })

    const searchBtn = document.getElementById('search-btn');
    searchBtn.addEventListener('click', () => {
      searchBtn.href = `/search/?value=${search.value}`
    });
});

function render(){
    
    const page = document.getElementById('page');
    const menu = Menu();

    const searchBarContainer = document.createElement('div');
    searchBarContainer.id = 'search-bar-container';

    const search = document.createElement('input');
    search.type = 'text';
    search.id = 'search';
    search.placeholder = 'Sök plats...';

    const searchBtn = document.createElement('a');
    searchBtn.id = 'search-btn'
    searchBtn.innerHTML = 'sök'
    searchBtn.href = '/search/?value='

    const searchResults = document.createElement('div');
    searchResults.id = 'search-results';
    searchResults.style.display = 'none';

    const bg = document.createElement('img');
    bg.id = 'bg';
    bg.src = './icons/bg.svg';

    page.append(menu);
    searchBarContainer.append(search);
    searchBarContainer.append(searchBtn);
    page.append(searchBarContainer);
    page.append(searchResults);
    page.append(bg);
}


