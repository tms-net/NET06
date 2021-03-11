import { FieldValidator } from './fieldValidator.js';

var allValidators = [
    new FieldValidator("name", [FieldValidator.Required, FieldValidator.RegExp("/[a-z   ]?/")]),        
    new FieldValidator("last_name", [FieldValidator.RegExp("/[a-zA-Z]?/")]),
    new FieldValidator("accept", [FieldValidator.Required]),
];

var allStyles = [];
window.onload = function() {
    allValidators.forEach(validator => validator.initialize());
    document.getElementById("myForm").onsubmit = () => validate();
}

function validate(evt) {
    allValidators.forEach(validator => validator.validate());
    return !allValidators.some(validator => !validator.isValid());
}