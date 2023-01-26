'use strict';
import Card from '../components/Card.js'
import Menu from '../components/Menu.js';

window.addEventListener("DOMContentLoaded", async () => {
    const sights = await fetch('https://localhost:7260/api/Attraction').then(res => res.json());
    const countries = await fetch('https://localhost:7260/api/Country').then(res => res.json());
    const cities = await fetch('https://localhost:7260/api/City').then(res => res.json());
    
    const queryString = window.location.search;
    const searchVal = new URLSearchParams(queryString).get('value');

    render(sights, countries, cities, searchVal);
});

function render(sights, countries, cities, searchVal){
    const page = document.getElementById('page');
    const menu = Menu();

    const card = Card();

    const searchTable = document.createElement('table');
    searchTable.id = 'search-table';

    sights.forEach((attraction) => {
        const row = document.createElement('tr')
        
        const title = document.createElement('td');
        const link = document.createElement('a');
        link.className = 'link';
        link.innerHTML = attraction.title;
        link.href = `/comment/?id=${attraction.id}`;
        
        title.append(link);
        row.append(title);
        searchTable.append(row);
    });
    countries.forEach((country) => {
      const row = document.createElement('tr')
        
      const name = document.createElement('td');
      const link = document.createElement('a');
      link.className = 'link';
      link.innerHTML = country.name;
      link.href = `/sights/?id=${country.id}`;

      name.append(link);
      row.append(name);
      searchTable.append(row);
    })
    cities.forEach((city) => {
      const row = document.createElement('tr')
        
      const name = document.createElement('td');
      const link = document.createElement('a');
      link.className = 'link';
      link.innerHTML = city.name;
      link.href = `/sights/?id=${city.id}`;

      name.append(link);
      row.append(name);
      searchTable.append(row);
    })

    let filter = searchVal.toUpperCase();
    let tr = searchTable.getElementsByTagName("tr");
    let i, txtValue;
    for (i = 0; i < tr.length; i++) {
        let td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
            tr[i].style.display = "";
            } else {
            tr[i].style.display = "none";
            }
        }       
    }

    page.append(menu);
    card.append(searchTable);
    page.append(card);
}