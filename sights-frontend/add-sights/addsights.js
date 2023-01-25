'use strict';

import Card from "../components/Card.js";
import icon from '../components/logo.js';
import createBtn from "../components/primaryButton.js";
import cancelBtn from "../components/secondaryButton.js";

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

    const inputTitle= document.createElement("input");
    inputTitle.setAttribute("type", "text");
    inputTitle.placeholder ="Title"
    card.append(inputTitle);

    const inputCountry = document.createElement("input");
    inputCountry.setAttribute("type", "text");
    inputCountry.placeholder ="Country"
    card.append(inputCountry);

    const inputCity = document.createElement("input");
    inputCity.setAttribute("type", "text");
    inputCity.placeholder ="City"
    card.append(inputCity);

    const inputDescription = document.createElement("textarea");
    inputDescription.id = 'inputDescription';
    inputDescription.setAttribute("type", "text");
    inputDescription.placeholder ="Description"
    card.append(inputDescription);

    const buttonContainer = document.createElement('div');
    buttonContainer.id = 'button-container';
    const creBtn = createBtn('button');
    const backBtn = cancelBtn('a');
    creBtn.innerHTML ='Add sight'
    creBtn.id = 'cre-btn';
    
    backBtn.innerHTML ='Cancel'
    backBtn.href = '/'
    buttonContainer.append(creBtn);
    buttonContainer.append(backBtn);
    card.append(buttonContainer);
 

    page.append(logo);
    page.append(card);

}