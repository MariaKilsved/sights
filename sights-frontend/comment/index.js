'use strict';

import Menu from "../components/Menu.js";
import Sight from "../components/Sight.js";
import CommentBox from '../components/CommentBox.js'
let upVoteNr = 0;
let downVoteNr = 0;

window.addEventListener("DOMContentLoaded", async () => {
    render();

    const upVote = document.getElementById('likesUpIMG');
    upVote.addEventListener('click', async () => {
        let upVote = document.getElementById('likesUp');
        upVoteNr++;
        upVote.textContent = upVoteNr;
        console.log(upVoteNr);
    })

    const downVote = document.getElementById('likesDownIMG');
    downVote.addEventListener('click', async () => {
        let downVote = document.getElementById('likesDown');
        downVoteNr++;
        downVote.textContent = downVoteNr;
        console.log(downVoteNr);
    })
});

function render(){
    
    const page = document.getElementById('page');
    const menu = Menu();
    const sight = Sight();

    const commentContainer = document.createElement('div');
    commentContainer.id = 'commentContainer';

    const commentBox = CommentBox();
    const com2 = CommentBox();
    const com3 = CommentBox();

    commentContainer.append(commentBox);
    commentContainer.append(com2);

    page.append(menu);
    page.append(sight);
    page.append(commentContainer);


}