'use strict';

import Menu from "../components/Menu.js";
import Sight from "../components/Sight.js";
import CommentBox from '../components/CommentBox.js'
import AddCommentBox from "../components/AddCommentBox.js";
import { post, get, deleteRequest, putRequest } from '../lib/request.js';
import commentSamplePostData from './mock-data/MockData.js'

window.addEventListener("DOMContentLoaded", async () => {
    
    let likes = await get(`https://localhost:7260/api/Like`);
    console.log(likes)
    const queryString = window.location.search;
    const attractionId = new URLSearchParams(queryString).get('id');
    const attraction = await get(`https://localhost:7260/api/Attraction/${attractionId}`)
    const user = JSON.parse(localStorage.getItem('userinfo'));
    console.log(user)
    let isLiked;
    if(user){
        isLiked = await hasLiked(user, attractionId, likes);
    }
    console.log(isLiked)
   
    await render(user, isLiked, attraction);

    const upVote = document.getElementById('likesUpIMG');
    upVote.addEventListener('click', async () => {
        let upVote = document.getElementById('likesUp');

        if (!isLiked) {
            const userLike = {
                userId: user.userId,
                AttractionId: attractionId,
                like1: 1,
            }

            let upVoteNr = upVote.innerHTML;
            upVoteNr++;
            upVote.innerHTML = upVoteNr;

            await post(`https://localhost:7260/api/Like`, userLike);
        }
        else {
            let upVoteNr = upVote.innerHTML;
            upVoteNr--;
            upVote.innerHTML = upVoteNr;
            
            let likeId;
            likes.forEach(like =>{
                if(like.userId === user.userId && attractionId === like.attractionId){
                    likeId = like.id;
                }
            });

            await deleteRequest(`https://localhost:7260/api/Like/${likeId}`);
        }

        likes = await get(`https://localhost:7260/api/Like`);
        isLiked = hasLiked(user, attractionId, likes)
        console.log(isLiked)

        //const likes = await get(`https://localhost:7260/api/Like`);

        //const userInfo = JSON.parse(localStorage.getItem('userinfo'));



        // likeLoop:
        // for (let i = 0; i < likes.length; i++){
        //     if (likes[i].userId === userInfo.userId && attractionId == likes[i].attractionId){
        //         if (likes[i].like1 == 0){
        //          await post(`https://localhost:7260/api/Like/${likes[i].id}`, userLike);
        //          break likeLoop;
        //         }
        //     }


        //     if (i == likes.length -1){
        //         const userLike = {
        //             userId: userInfo.userId,
        //             attractionId,
        //             like1: 1
        //         }
        //       await post(`https://localhost:7260/api/Like`, userLike);
        //     } 
        // }
    })

    //DOWNVOTE

    // const downVote = document.getElementById('likesDownIMG');
    // downVote.addEventListener('click', async () => {
    //     let downVote = document.getElementById('likesDown');

    //     if (canDownVote){
    //         downVoteNr++;
    //         downVote.textContent = downVoteNr;
    //         canDownVote = false;
    //     }
    //     else {
    //         downVoteNr--;
    //         downVote.textContent = downVoteNr;
    //         canDownVote = true;
    //     }
    // })

    const sendMessageBtn = document.getElementById('sendMessageBtn');
    sendMessageBtn.addEventListener('click', async () => {
        const commentContainer = document.getElementById('commentContainer');
        const addCommentText = document.getElementById('addCommentBox');
        const newComment = CommentBox(addCommentText.value);
        commentContainer.appendChild(newComment);
        commentSamplePostData.content = addCommentText.value;

        const storedUser = JSON.parse(localStorage.getItem('userinfo'));
        const queryString = window.location.search;
        const attractionId = new URLSearchParams(queryString).get('id');

        const comment = {
                userId: storedUser.userId,
                content: addCommentText.value,
                attractionId: attractionId,
        }   

         await post(`https://localhost:7260/api/comment`, comment);

        addCommentText.value = '';
    })
});

function hasLiked(user, attractionId, likes){
    let hasLiked = false;
    console.log(attractionId)
    likes.forEach(like => {
        console.log(like.userId)
        if(like.userId === user.userId && attractionId === like.attractionId){
            if(like.like1 === 1) hasLiked = true;
            console.log(hasLiked)
        }
    });

    return hasLiked;
}

async function render(user, isLiked, attraction){
    const comments = await get(`https://localhost:7260/api/Comment`);
    const likes = await get(`https://localhost:7260/api/Like`);
    const queryString = window.location.search;
    const attractionId = new URLSearchParams(queryString).get('id');
    
    const page = document.getElementById('page');
    const menu = Menu();
    const sight = Sight(likes, attractionId);
    const addCommentBox = AddCommentBox();

    const commentContainer = document.createElement('div');
    commentContainer.id = 'commentContainer';

    for (let i = 0; i < comments.length; i++){
        if (comments[i].attractionId == attractionId){
            const commentBox = CommentBox(comments[i].content);
            commentContainer.append(commentBox);
        }
    }
    page.append(menu);
    page.append(sight);
    page.append(commentContainer);
    page.append(addCommentBox);
}