export default function AddCommentBox(){
    const addCommentBoxContainer = document.createElement('div');
    addCommentBoxContainer.id = 'addCommentBoxContainer';
    
    const addCommentBox = document.createElement('textarea');
    addCommentBox.id = 'addCommentBox';

    const sendMessage = document.createElement('button');
    sendMessage.id = 'sendMessageBtn';

    addCommentBoxContainer.appendChild(addCommentBox);
    addCommentBoxContainer.appendChild(sendMessage);

    return addCommentBoxContainer;
}