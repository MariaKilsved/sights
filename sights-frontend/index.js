'use strict';

import Menu from './components/Menu.js';
import primaryButton from './components/primaryButton.js';
import {get} from './lib/request.js';

window.addEventListener("DOMContentLoaded", async () => {

    const user = JSON.parse(window.localStorage.getItem('userinfo'));

    const sights = await fetch('https://localhost:7260/api/Attraction').then(res => res.json());
    const countries = await fetch('https://localhost:7260/api/Country').then(res => res.json());
    const cities = await fetch('https://localhost:7260/api/City').then(res => res.json());

    console.log(sights)

    render(user);

    const searchResults = document.getElementById('search-results');
    const attractionTable = document.createElement('div');
    attractionTable.id = 'attraction-table';

    sights.forEach((attraction) => {
        const row = document.createElement('div');
        row.className = "table-row";
        
        const title = document.createElement('div');
        title.className = "table-cell";
        const link = document.createElement('a');
        link.className = 'link';
        link.innerHTML = attraction.title;
        link.href = `/comment/?id=${attraction.id}`;
        
        title.append(link);
        row.append(title);
        attractionTable.append(row);
    });
    countries.forEach((country) => {
      const row = document.createElement('div');
      row.className = "table-row";
        
      const name = document.createElement('div');
      name.className = "table-cell";
      const link = document.createElement('a');
      link.className = 'link';
      link.innerHTML = country.name;
      link.href = `/sights/?location=${country.name}`;

      name.append(link);
      row.append(name);
      attractionTable.append(row);
    })
    cities.forEach((city) => {
      const row = document.createElement('div')
      row.className = "table-row";
        
      const name = document.createElement('div');
      name.className = "table-cell";
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
      tr = table.getElementsByClassName("table-row");
      var last;
      for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByClassName("table-cell")[0];
        if (td) {
          txtValue = td.textContent || td.innerText;
          if (txtValue.toUpperCase().indexOf(filter) > -1) {
            tr[i].style.display = "";
            last = tr[i];
          } else {
            tr[i].style.display = "none";
          }
        }     
      }

      last.style.paddingBottom = "10px";
    })

    const searchBtn = document.getElementById('search-btn');
    searchBtn.addEventListener('click', () => {
      searchBtn.href = `/search/?value=${search.value}`
    });
});

function render(user){
    
    const page = document.getElementById('page');
    const menu = Menu();

    const searchBarFrame = document.createElement("div");
    searchBarFrame.className = 'search-bar-frame';

    if(user ){
      const addBtn = primaryButton('a');
      addBtn.innerHTML='Add sight';
      addBtn.href='/add-sights'
      addBtn.classList.add("slim-btn");
      searchBarFrame.append(addBtn);
    }
    
    const searchBarContainer = document.createElement('div');
    searchBarContainer.id = 'search-bar-container';

    const search = document.createElement('input');
    search.type = 'text';
    search.id = 'search';
    search.placeholder = 'Search place...';

    const searchBtn = document.createElement('a');
    searchBtn.id = 'search-btn';
    const searchBtnImg = document.createElement('img');
    searchBtnImg.src = "./icons/search.svg";
    searchBtn.append(searchBtnImg);
    searchBtn.href = '/search/?value='

    const searchResults = document.createElement('div');
    searchResults.id = 'search-results';
    searchResults.style.display = 'none';

    searchBarContainer.append(search);
    searchBarContainer.append(searchBtn);
    searchBarFrame.append(searchBarContainer);
    searchBarFrame.append(searchResults);

    const bg = document.createElement('img');
    bg.id = 'bg';
    bg.src = './icons/bg.svg';

    const bgLogo = document.createElement("img");
    bgLogo.id = 'bg-logo';
    bgLogo.src = "./icons/Logo_text.svg";

    page.append(menu);
    page.append(searchBarFrame);
    page.append(bg);
    page.append(bgLogo);
}


