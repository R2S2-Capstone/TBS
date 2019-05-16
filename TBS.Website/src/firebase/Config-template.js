const firebase = require('firebase/app')
require('firebase/auth')

var config = {
    apiKey: "",
    authDomain: "",
    databaseURL: "",
    projectId: "",
    storageBucket: "",
    messagingSenderId: ""
};

export default firebase.initializeApp(config);