'use strict';

import Card from "../components/Card.js";
import icon from '../components/logo.js';

window.addEventListener("DOMContentLoaded", async () => {
    addSight();
});

function addSight(){
    const page = document.getElementById('page');
    const card = Card();
   
    const logo = icon(); 
    logo.className = 'logoClass' ;
    const addSight = document.createElement('p2');
    addSight.innerHTML = 'Add Sight';
    card.append(addSight)

    const inputHeadline= document.createElement("input");
    inputHeadline.setAttribute("type", "text");
    inputHeadline.placeholder ="Headline"
    card.append(inputHeadline);

    const inputCountry = document.createElement("input");
    inputCountry.setAttribute("type", "text");
    inputCountry.placeholder ="Country"
    card.append(inputCountry);

    const inputCity = document.createElement("input");
    inputCity.setAttribute("type", "text");
    inputCity.placeholder ="City"
    card.append(inputCity);

    const inputDescription = document.createElement("input");
    inputDescription.id = 'inputDescription';
    inputDescription.setAttribute("type", "text");
    inputDescription.placeholder ="Description"
    card.append(inputDescription);
 

    page.append(logo);
    page.append(card);
    
    

}