const initialState = {
    books:[
        // { id: 1, title:'Production-Ready Microservices', author:'Susan J.Fowler'},
        // { id: 2, title:'Release It', author: 'Michael T. Nygard'}
    ],
    loading : true
};

const reducer = (state = initialState, action) => {

    switch(action.type){
        case 'BOOKS_LOADED':
        return {
            books:action.payload,
            loading: false
        }
        default:
            return state;
    }
}

export default reducer;