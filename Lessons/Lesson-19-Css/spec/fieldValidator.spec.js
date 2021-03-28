import { FieldValidator } from "../src/fieldValidator.mjs";

// these tests will be run in browser environment, so DOM API will be available and it will allow to test DOM manipulation
// although tests will run much slower that regular unit tests, so they should be executed more rearly.

describe("FieldValidatorTests", function() {
  
    let testForm;

    function getFormDOM() {
        const form = document.createElement('form');
        form.innerHTML = `
          <label for="username">Username</label>
          <input type="text" id="username" />
          <input type="checkbox" id="checkbox" />
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

        //act
        validator.initialize();
        validator.validate();

        //assert
        expect(validator.isValid()).toBe(true);
        expect(document.getElementById("username").parentElement.tagName).toBe("SPAN");
    });

    it("should correctly check checkbox input field for Required validation", function() {
        // TODO: test functionality with DOM
    });

    ["username", "checkbox"].forEach((id) => {
        it(`should correctly check text ${id} field for Required validation`, function() {
            //arrage
            var validator = new FieldValidator(id, [FieldValidator.Required]);
            validator.initialize();

            //act
            validator.validate();
    
            //assert
            expect(validator.isValid()).toBe(false);
            expect(document.getElementById(id).parentElement.tagName).toBe("SPAN");
            expect(document.getElementById(id).nextSibling.tagName).toBe("SPAN");
        });
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