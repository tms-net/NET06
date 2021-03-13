import { FieldValidator } from "../src/fieldValidator.js";

// these tests will be run in browser environment, so DOM API will be available and it will allow to test DOM manipulation
// although tests will run much slower that regular unit tests, so they should be executed more rearly.

describe("FieldValidatorTests", function() {
  
    let testForm;

    function getFormDOM() {
        const form = document.createElement('form');
        form.innerHTML = `
          <label for="username">Username</label>
          <input id="username" />
          <button>Print Username</button>
        `;

        return form
      }

    beforeEach(function() {
        testForm = getFormDOM();
        document.body.appendChild(testForm);
    });

    afterEach(function() {
        testForm && testForm.remove();
    });

    
    it("should not check empty fields for RegExp validation", function() {
        //arrage
        var validator = new FieldValidator("username", [FieldValidator.RegExp("\d+")]);
        validator.initialize();

        //act
        validator.validate();

        //assert
        expect(validator.isValid()).toBe(true);
    });

    it("should correctly check checkbox input field for Required validation", function() {
        // TODO: test functionality with DOM
    });

    it("should correctly check text input field for Required validation", function() {
        // TODO: test functionality with DOM
    });
    
    it("should check all provided validators", function() {
        //arrage
        var goodValueValidator = jasmine.createSpy("goodValueValidator", () => true);
        var badValueValidator = jasmine.createSpy("badValueValidator", () => false);
        var validator = new FieldValidator("username", [badValueValidator, goodValueValidator]);
        validator.initialize();
  
        //act
        validator.validate();
  
        //assert
        expect(badValueValidator).toHaveBeenCalled();
        expect(goodValueValidator).toHaveBeenCalled();
    });
});