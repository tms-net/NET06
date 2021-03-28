import { FieldValidator } from "../src/fieldValidator.mjs";

// these tests will be run in Node.js environment, so DOM elements and window context won't be available
// it will allow to run tests fast and as many times as possible

describe("FieldValidatorUnitTests", function() {
  
    beforeEach(function() {
        
    });

    
    describe("should not check empty fields for RegExp validation", function() {

      // defining test cases as array of corresponding objects
      [{}, {value: null}, {value: ""}].forEach(function(element) {

        it(`for field with value: ${element.value}`, function() {
          //arrage
          var validator = FieldValidator.RegExp("\d+");

          //act
          var result = validator(element);

          //assert
          expect(result).toBe(true);
        })
      })
    });

    it("should correctly check checkbox input field for Required validation", function() {
      // TODO: test pure functionality
    });

    it("should correctly check text input field for Required validation", function() {
      // TODO: test pure functionality
    });    
});