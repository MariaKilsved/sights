'use strict';
import Card from "../components/Card.js";
import icon from '../components/logo.js';
import registerBtn from "../components/primaryButton.js";
import cancelBtn from "../components/secondaryButton.js";
import {post} from '../lib/request.js'
window.addEventListener("DOMContentLoaded", async () => {
    register();

    const regbtn = document.getElementById('reg-btn');
    regbtn.addEventListener('click', async()=>{
        const accountname = document.getElementById('input-accountname').value;
        const passwordAccount = document.getElementById('input-password').value;
        const confirmPassword = document.getElementById('input-confirmpassword').value;



        if (passwordAccount === confirmPassword){
            //window.location.href='/'
            const response = await post(`https://localhost:7260/api/User?`,{username: accountname,password: confirmPassword});
        }
        else
        {
            window.alert('Password is not the same')
        }

    })
});

function register(){
    const page = document.getElementById('page');
    const card = Card();
    
    
    const newAccount = document.createElement('p1');
    newAccount.innerHTML = 'Enter Account Information';
    card.append(newAccount)

    const inputAccountName= document.createElement("input");
    inputAccountName.id = 'input-accountname';
    inputAccountName.setAttribute("type", "text");
    inputAccountName.placeholder ="Account name"
    card.append(inputAccountName);

    const inputPasswordAccount = document.createElement("input");
    inputPasswordAccount.id = 'input-password';
    inputPasswordAccount.setAttribute("type", "password");
    inputPasswordAccount.placeholder ="Password"
    card.append(inputPasswordAccount);

    const inputConfirmPassword = document.createElement("input");
    inputConfirmPassword.id = 'input-confirmpassword';
    inputConfirmPassword.setAttribute("type", "password");
    inputConfirmPassword.placeholder ="Confirm Password"
    card.append(inputConfirmPassword);
    page.append(card);
   
    
    const logo = icon(); 
    logo.className = 'logoClass' ;
    page.append(logo);

    const buttonContainer = document.createElement('div');
    buttonContainer.id = 'button-container';
    const regbtn = registerBtn('button');
    regbtn.id = 'reg-btn';
    const canbtn = cancelBtn('a');
    canbtn.href='/'
    regbtn.innerHTML ='Register'
    canbtn.innerHTML ='Cancel'
    buttonContainer.append(regbtn);
    buttonContainer.append(canbtn);
    card.append(buttonContainer);

}