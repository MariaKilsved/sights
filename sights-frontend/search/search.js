'use strict';
import Card from '../components/Card.js'
import Menu from '../components/Menu.js';
import {get} from '../lib/request.js';

window.addEventListener("DOMContentLoaded", async () => {
    const sights = await get('https://localhost:7260/api/Attraction/ByLikes');
    const countries = await fetch('https://localhost:7260/api/Country').then(res => res.json());
    const cities = await fetch('https://localhost:7260/api/City').then(res => res.json());

    
    const queryString = window.location.search;
    const searchVal = new URLSearchParams(queryString).get('value');

    render(sights, countries, cities, searchVal);

    
    const searchTable = document.getElementById('search-table');
    let filter = searchVal.toUpperCase();
    let tr = searchTable.getElementsByTagName("tr");
    let i, txtValue;
    for (i = 0; i < tr.length; i++) {
        let td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.querySelector('#title').innerText;
            console.log(txtValue);
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
            tr[i].style.display = "";
            } else {
            tr[i].style.display = "none";
            }
        }       
    }
});



function render(sightsWithLikes, countries, cities, searchVal){
    const page = document.getElementById('page');
    const menu = Menu();

    const card = Card();

    const searchTable = document.createElement('table');
    searchTable.id = 'search-table';
    
    sightsWithLikes.forEach((attraction) => {
        const row = document.createElement('tr')
        row.className='row'
        
        const container = document.createElement('td');
        const title = document.createElement('p');
        title.id = 'title';
        title.innerHTML = attraction.attraction.title;
        const likes = document.createElement('td');
        const info = document.createElement('p');
        info.id = 'description';
        info.innerHTML=attraction.attraction.description;
        const link = document.createElement('a');
        link.href = `/comment/?id=${attraction.attraction.id}`;
        link.className = 'link';
        
        const nrOfLikes = document.createElement('p');
        nrOfLikes.innerHTML = attraction.likeCount + " likes";

        container.append(link);
        link.append(title);
        link.append(info);
        row.append(container);
        likes.append(nrOfLikes);
        row.append(likes);
        searchTable.append(row);
    });
    countries.forEach((country) => {
      const row = document.createElement('tr')
         
      const container = document.createElement('td');
      container.id = 'container';
      const name = document.createElement('td');
      name.id = 'title';
      name.innerHTML = country.name;
      const link = document.createElement('a');
      link.className = 'link';
      link.href = `/sights/?location=${country.name}`;

      link.append(name);
      container.append(link);
      row.append(container);
      searchTable.append(row);
    })
    cities.forEach((city) => {
      const row = document.createElement('tr')
        
      const container = document.createElement('td');
      container.id = 'container';
      const name = document.createElement('p');
      name.id = 'title';
      name.innerHTML = city.name;
      const link = document.createElement('a');
      link.className = 'link';
      link.href = `/sights/?location=${city.name}`;

      link.append(name);
      container.append(link);
      row.append(container);
      searchTable.append(row);
    })


    page.append(menu);
    card.append(searchTable);
    page.append(card);
}
