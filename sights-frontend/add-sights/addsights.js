'use strict';

import Card from "../components/Card.js";

import Menu from '../components/Menu.js';
import icon from '../components/logo.js';
import createBtn from "../components/primaryButton.js";
import cancelBtn from "../components/secondaryButton.js";
import {post} from '../lib/request.js'

window.addEventListener("DOMContentLoaded", async () => {
    render();
    const createBtn = document.getElementById('create-btn');
    const user = JSON.parse(window.localStorage.getItem('userinfo'));
    createBtn.addEventListener('click', async()=>{
        const inputTitle = document.getElementById('input-title').value;
        const inputCountry = document.getElementById('input-country').value.trim();
        const inputCity = document.getElementById('input-city').value.trim();
        const inputDescription = document.getElementById('inputDescription').value;

        const storedUser = JSON.parse(localStorage.getItem('userinfo'));

        const sightObj = {userId: user.userId, Title: inputTitle,Country:{name:inputCountry},City:{name: inputCity},Description: inputDescription}
        const response = await post(`https://localhost:7260/api/Attraction?`, sightObj, storedUser.encryptedToken);
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
    const addSightText = document.createElement('p');
    addSightText.className = "p2";
    addSightText.innerHTML = 'Add Sight';
    card.append(addSightText)

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
    const createButton = createBtn('button');
    const backBtn = cancelBtn('a');
    createButton.innerHTML ='Add sight'
    createButton.id = 'create-btn';
    
    backBtn.innerHTML ='Cancel'
    backBtn.href = '/'
    buttonContainer.append(createButton);
    buttonContainer.append(backBtn);
    card.append(buttonContainer);

    const bgIMG = document.createElement('img');
    bgIMG.id = 'bg';
    bgIMG.src = '../icons/bg.svg';
 
    page.append(menu);
    page.append(card);

    page.append(bgIMG);
   

}
