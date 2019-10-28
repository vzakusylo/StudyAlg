create-react-app re-store
npm install prop-types react-router-dom redux react-redux

120
 /actions - action creators
 /reducers = reducers
 / services 
 / utils
 / components
   /app
   /pages
   /error-boundry
   /error-indicator
   /spinner
   /bookstore-service-context
   /hoc


   React: setState()
   -----------------
   {a:0, b:0} // initial state
   setState({a:100});
   {a:100, b:0}

   Redux: reducer()
   -----------------------
   {a:0, b:0} // initial state
   
   const reducer = (state, action) => {
     return { a:100 }
   }
   {a:100}

   const reducer = (state, action) => {
     return { a:100, b: state.b }
   }
   {a:100,b:0}

   const reducer = (state, action) => {
     return { ...state, a:100 };
   }
   {a:100,b:0}