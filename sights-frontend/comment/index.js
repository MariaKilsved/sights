'use strict';

import Menu from "../components/Menu.js";
import Sight from "../components/Sight.js";

window.addEventListener("DOMContentLoaded", async () => {
    render();
});

function render(){
    
    const page = document.getElementById('page');
    const menu = Menu();
    const sight = Sight();

    page.append(menu);
    page.append(sight);

}