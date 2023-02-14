'use strict';

import Menu from "../components/Menu.js";
import Sight from "../components/Sight.js";
import CommentBox from '../components/CommentBox.js'
import AddCommentBox from "../components/AddCommentBox.js";
import { post, get, deleteRequest, putRequest } from '../lib/request.js';

window.addEventListener("DOMContentLoaded", async () => {
    
    const queryString = window.location.search;
    const attractionId = new URLSearchParams(queryString).get('id');
    let likes = await get(`https://localhost:7260/api/Like`);
    const comments = await get(`https://localhost:7260/api/Comment/ByAttraction?attractionId=${attractionId}`);
    const attraction = await get(`https://localhost:7260/api/Attraction/${attractionId}`)
    const user = JSON.parse(localStorage.getItem('userinfo'));
    let isLiked;
    let isDownVoted;
    if(user){
        isDownVoted = await hasDownVoted(user, attractionId, likes);
        isLiked = await hasLiked(user, attractionId, likes);
    }
   
    render(user, isLiked, attraction, likes, comments);

     const commenctContainer = document.getElementById('commentContainer');
    if (!commenctContainer.hasChildNodes()) commenctContainer.style.display = 'none';

    const upVote = document.getElementById('likesUpIMG');
    upVote.addEventListener('click', async () => {
        if (!user) window.alert('Need to login to like');
    })

    const downVote = document.getElementById('likesDownIMG');
    downVote.addEventListener('click', async () => {
        if (!user) window.alert('Need to login to dislike');
    })

    if(user){
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
    
                await post(`https://localhost:7260/api/Like`, userLike, user.encryptedToken);
            }
            else {
                let upVoteNr = upVote.innerHTML;
                upVoteNr--;
                upVote.innerHTML = upVoteNr;
                
                let likeId;
                likes.forEach(like =>{
                    if(like.userId == user.userId && attractionId == like.attractionId){
                        likeId = like.id;
                    }
                });
    
                await deleteRequest(`https://localhost:7260/api/Like/${likeId}?attractionId=${attractionId}&userId=${user.userId}`, user.encryptedToken);
            }
    
            likes = await get(`https://localhost:7260/api/Like`);
            isLiked = hasLiked(user, attractionId, likes)
        })

        const downVote = document.getElementById('likesDownIMG');
        downVote.addEventListener('click', async () => {
            let downVote = document.getElementById('likesDown');
    
            if (!isDownVoted) {
                const userLike = {
                    userId: user.userId,
                    AttractionId: attractionId,
                    like1: 0,
                }
    
                let downVoteNr = downVote.innerHTML;
                downVoteNr++;
                downVote.innerHTML = downVoteNr;
    
                await post(`https://localhost:7260/api/Like`, userLike, user.encryptedToken);
            }
            else {
                let downVoteNr = downVote.innerHTML;
                downVoteNr--;
                downVote.innerHTML = downVoteNr;
                
                let likeId;
                likes.forEach(like =>{
                    if(like.userId == user.userId && attractionId == like.attractionId){
                        likeId = like.id;
                    }
                });
    
                await deleteRequest(`https://localhost:7260/api/Like/${likeId}?attractionId=${attractionId}&userId=${user.userId}`, user.encryptedToken);
            }
    
            likes = await get(`https://localhost:7260/api/Like`);
            isDownVoted = hasDownVoted(user, attractionId, likes)
        })
        
        const sendMessageBtn = document.getElementById('sendMessageBtn');
        sendMessageBtn.innerHTML ="Submit";
        sendMessageBtn.className = "primary-btn";
        sendMessageBtn.addEventListener('click', async () => {
        const addCommentText = document.getElementById('addCommentBox');
            if(addCommentText.value != ''){
                
            const storedUser = JSON.parse(localStorage.getItem('userinfo'));
            const queryString = window.location.search;
            const attractionId = new URLSearchParams(queryString).get('id');

            const commentContainer = document.getElementById('commentContainer');
            const newComment = CommentBox(addCommentText.value, storedUser.userName);
            commentContainer.appendChild(newComment);
    
            const comment = {
                    userId: storedUser.userId,
                    content: addCommentText.value,
                    attractionId: attractionId,
            }   
            await post(`https://localhost:7260/api/comment`, comment, storedUser.encryptedToken);
    
            addCommentText.value = '';
            if (commenctContainer.hasChildNodes()) commenctContainer.style.display = 'block';
            }
        })
    }


});

function hasLiked(user, attractionId, likes){
    let hasLiked = false;
    likes.forEach(like => {
        if(like.userId == user.userId && attractionId == like.attractionId){
            if(like.like1 === 1) hasLiked = true;
        }
    });

    return hasLiked;
}

function hasDownVoted(user, attractionId, likes){
    let hasDownVoted = false;
    likes.forEach(like => {
        if(like.userId == user.userId && attractionId == like.attractionId){
            if(like.like1 === 0) hasDownVoted = true;
        }
    });

    return hasDownVoted;
}

function render(user, isLiked, attraction, likes, comments){
    const queryString = window.location.search;
    const attractionId = new URLSearchParams(queryString).get('id');
    
    const page = document.getElementById('page');
    const menu = Menu();
    const sight = Sight(likes, attractionId, attraction);
    const addCommentBox = AddCommentBox();

    const commentContainer = document.createElement('div');
    commentContainer.id = 'commentContainer';

    comments.forEach((comment) => {
        const commentBox = CommentBox(comment.comment.content, comment.username);
        commentContainer.append(commentBox);
    })

    page.append(menu);
    page.append(sight);
    page.append(commentContainer);
    if(user) page.append(addCommentBox);
}
