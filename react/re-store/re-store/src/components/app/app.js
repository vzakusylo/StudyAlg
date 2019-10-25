import React from 'react';
import {Route, Switch} from 'react-router-dom';
import ShopHeader from '../shop-header';
import './app.css'
//import {withBookstoreService} from '../hoc';
import {HomePage, CartPage} from '../pages';

const App = ({bookstoreService}) => {
    //console.log(bookstoreService.getBooks());
    
    return (
        <main role="main" className="container">
            <ShopHeader numItems={5} total={210}/>
            <Switch>
                <Route path="/" component={HomePage} exact/>
                <Route path="/cart" component={CartPage} exact/>
            </Switch>
        </main>
    );
};

//export default withBookstoreService()(App);
export default App;