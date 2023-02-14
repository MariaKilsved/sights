'use strict';

const urlBase = "https://hellofrommartin.azurewebsites.net";
const urlID = urlBase + "/id";
const urlLogin = urlBase + "/api/login";
const urlFriends = urlBase + "/api/friends";
const urlQuotes = urlBase + "/api/quotes";

async function myFetch(url, jwtEncryptedToken = null, method = null, body = null) {
  try {

    let _headers = { 'content-type': 'application/json' };
    if (jwtEncryptedToken != null)
    {
      _headers['Authorization'] = `Bearer ${jwtEncryptedToken}`;
    }

    let res = await fetch(url, {
      method: method ?? 'GET',
      headers: _headers,
      body: body ? JSON.stringify(body) : null
    });

    if (res.ok) {

      console.log("Request successful");

      if (method == 'PUT' || method == 'DELETE')
        //request is successful, but WebAPI is not send a response, so I return the body which represenst the effect on the database
        return body;

      //get the data from server
      let data = await res.json();
      return data;
    }
    else {

      console.log(`Failed to recieved data from server: ${res.status}`);
      //alert(`Failed to recieved data from server: ${res.status}`);
    }
  }
  catch (err) {

    console.log(`Failed to recieved data from server: ${err.message}`);
    //alert(`Failed to recieved data from server: ${err.message}`);
  }
}


(async ()=>   {

    console.log("\nTesting Login a User");
    let userToken = await myFetch(urlLogin + '/loginuser', null, 'POST', { userName: "User1", password: "pa$$W0rd"});
    console.log(userToken);

  })();