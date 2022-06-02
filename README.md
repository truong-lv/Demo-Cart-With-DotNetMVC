# Demo-Cart-With-DotNetMVC (Training MWG)
## Book-store cart
- Apply knowledge about session, cookie, localStorage/sessionStorage:
   + Login/Logout (session, cookie)
   + Add to cart: 
      * With user is not logged in: save to localStorage
      *  With user is logged in: save to database and if previously there are carts stored on localStorage it will automatically load into the database for the user.
   + Restore comments (sessionStorage): user enters a comment (haven't clicked submit comment) and returns to the homepage (or other page), when the user returns to this comment page again the comment will be restored.
   + Show ads for the first time when accessing the page and will show again when the user does not refuse to receive ads (cookie).
