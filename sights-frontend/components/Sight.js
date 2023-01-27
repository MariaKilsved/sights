export default function Sight(likesCount, attractionId){
    let upVotes = 0
    let downVotes = 0;

    for (let i = 0; i < likesCount.length; i++){
        if (likesCount[i].attractionId == attractionId){
            if (likesCount[i].like1 === 0){
                downVotes++;
            }
            else upVotes++;
        }
    }

    const card = document.createElement('div');
    card.id = 'sight';

    const likesContainer = document.createElement('div');
    likesContainer.id = 'likesContainer';

    const likesUpContainer = document.createElement('div');
    likesUpContainer.id = 'likesUpContainer';

    const likeUpIMG = document.createElement('img');
    likeUpIMG.id = 'likesUpIMG';
    likeUpIMG.src = '../Figma images/Thumbs Up.png';

    const likesUp = document.createElement('p');
    likesUp.id = 'likesUp';
    likesUp.innerHTML = upVotes.toString();

    likesUpContainer.append(likesUp);
    likesUpContainer.append(likeUpIMG);

    const likesDownContainer = document.createElement('div');
    likesDownContainer.id = 'likesDownContainer';

    const likeDownIMG = document.createElement('img');
    likeDownIMG.id = 'likesDownIMG';
    likeDownIMG.src = '../Figma images/Thumbs Down.png';
        
    const likesDown = document.createElement('p');
    likesDown.id = 'likesDown';
    likesDown.innerHTML = downVotes.toString();

    likesDownContainer.append(likeDownIMG);
    likesDownContainer.append(likesDown);


    likesContainer.appendChild(likesUpContainer);
    likesContainer.appendChild(likesDownContainer);
    card.append(likesContainer);


    return card;
}