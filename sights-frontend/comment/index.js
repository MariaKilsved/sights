'use strict';

import Menu from "../components/Menu.js";
import Sight from "../components/Sight.js";
import CommentBox from '../components/CommentBox.js'

window.addEventListener("DOMContentLoaded", async () => {
    render();
});

function render(){
    
    const page = document.getElementById('page');
    const menu = Menu();
    const sight = Sight();
    const commentBox = CommentBox();
    const com2 = CommentBox();



    page.append(menu);
    page.append(sight);
    page.append(commentBox);
    page.append(com2);


}