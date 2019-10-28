import React, {Component} from 'react';
import BookListItem from '../book-list-item'
import Spinner from '../spinner'
import './book-list.css';
import {connect} from 'react-redux';
import { withBookstoreService } from '../hoc';
import {booksLoaded, booksRequested} from "../../actions";
import {compose} from '../../utils';
import './book-list.css';

class BookList extends Component{

    componentDidMount(){        
        const {bookstoreService, booksLoaded} = this.props;
        bookstoreService.getBooks().then((data)=>{
            booksLoaded(data);
        });
    }
    
    render(){
        const {books, loading} = this.props;

        if  (loading){
            return <Spinner/>
        }

        return(
            <ul className="book-list">
                {
                    books.map((book)=> {
                        return (
                            <li key={book.id}><BookListItem book={book}/></li>
                        )
                    })
                }
            </ul>
        );
    };
};
const mapStateToProps = ({books, loaading}) => {
    return { books, loaading };
};
const mapDispatchToProps = { booksLoaded, booksRequested };
export default compose(
    withBookstoreService(),
    connect(mapStateToProps, mapDispatchToProps)
)(BookList)
