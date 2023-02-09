'use strict';

import Card from "../components/Card.js";

import Menu from '../components/Menu.js';
import icon from '../components/logo.js';
import createBtn from "../components/primaryButton.js";
import cancelBtn from "../components/secondaryButton.js";
import {post} from '../lib/request.js'

window.addEventListener("DOMContentLoaded", async () => {
    render();
    const creBtn = document.getElementById('cre-btn');
    const user = JSON.parse(window.localStorage.getItem('userinfo'));
    creBtn.addEventListener('click', async()=>{
        const ititle = document.getElementById('input-title').value;
        const icountry = document.getElementById('input-country').value.trim();
        const icity = document.getElementById('input-city').value.trim();
        const idescription = document.getElementById('inputDescription').value;

        const sightObj = {userId: user.userId, Title: ititle,Country:{name:icountry},City:{name: icity},Description: idescription}
        const response = await post(`https://localhost:7260/api/Attraction?`, sightObj);
        window.alert('Your sight added')
        window.location.href='/'

     })

});

function render(){
    const page = document.getElementById('page');
    const card = Card();
    const menu = Menu();

    
    const logo = icon(); 
    logo.className = 'logoClass' ;
    const addSight = document.createElement('p');
    addSight.className = "p2";
    addSight.innerHTML = 'Add Sight';
    card.append(addSight)

    const inputTitle= document.createElement("input");
    inputTitle.id ='input-title';
    inputTitle.setAttribute("type", "text");
    inputTitle.placeholder ="Title"
    card.append(inputTitle);

    const inputCountry = document.createElement("input");
    inputCountry.id ='input-country';
    inputCountry.setAttribute("type", "text");
    inputCountry.placeholder ="Country"
    card.append(inputCountry);

    const inputCity = document.createElement("input");
    inputCity.id ='input-city';
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

    const bg = document.createElement('img');
    bg.id = 'bg';
    bg.src = '../icons/bg.svg';
 
    page.append(menu);
    //page.append(logo);
    page.append(card);

    page.append(bg);
   

}