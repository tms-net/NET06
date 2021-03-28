import './style.css'
import React from 'react';
import ReactDOM from 'react-dom';
import { FieldValidator } from './fieldValidator.mjs';

class Form extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            errors: {}
        }        
    }

    validate() {
        return false;
        // TODO: rewrite validate() function to work with React
        // it will require to refactor (rewrite) FieldValidator
    }

    submitForm(evt) {
        if (!this.validate()){
            evt.preventDefault();
        }
    }

    render() {
        return (
        <form className="form" onSubmit= {this.submitForm.bind(this)}>
            <fieldset>
                <label htmlFor="name">Name:</label>
                <input type="text" name="name" id="name"/>
                {/* error should be here */}

                <label htmlFor="last_name">Last Name:</label>
                <input type="text" name="last_name" id="last_name"/>            
                {/* error should be here */}

                <label htmlFor="accept">Accept terms and conditions.</label>
                <input type="checkbox" name="accept" id="accept"/>                
                {/* error should be here */}
            </fieldset>
            
            <input type="submit" value="Submit" />
        </form>
      );
    }
}

ReactDOM.render(
  <Form validators={[
        new FieldValidator("name", [FieldValidator.Required, FieldValidator.RegExp(/^[A-Za-z]+$/)]),        
        new FieldValidator("last_name", [FieldValidator.RegExp("^[A-Za-z]+$")]),
        new FieldValidator("accept", [FieldValidator.Required])
    ]}/>,
  document.getElementById('myForm')
);

/*function validate(evt) {
    allValidators.forEach(validator => validator.validate());
    return !allValidators.some(validator => !validator.isValid());
}*/