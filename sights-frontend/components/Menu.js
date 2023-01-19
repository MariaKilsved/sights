import primaryButton from "./primaryButton.js";
import secondaryButton from "./secondaryButton.js";

export default function Menu(){
    const menu = document.createElement('div');
    menu.id = 'menu';

    const logIn = primaryButton('a');
    logIn.innerHTML = 'login';
    logIn.href = '/login'
    
    const signup = secondaryButton('a');
    signup.innerHTML = 'signup';
    signup.href = '/register'

    const logo = document.createElement('img');
    logo.id = 'logo';
    logo.src = '/icons/Logo.svg'

    menu.append(logo);
    menu.append(logIn);
    menu.append(signup);

    return menu;
}