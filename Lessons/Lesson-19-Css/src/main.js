import './style.css'
import { FieldValidator } from './fieldValidator';

var allValidators = [
    new FieldValidator("name", [FieldValidator.Required, FieldValidator.RegExp(/^[A-Za-z]+$/)]),        
    new FieldValidator("last_name", [FieldValidator.RegExp("^[A-Za-z]+$")]),
    new FieldValidator("accept", [FieldValidator.Required])
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