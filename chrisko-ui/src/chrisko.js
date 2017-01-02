import * as api from './api';

const CHRISKO_CREATED = "CHRISKO_CREATED";
const UPDATE_STATE = "UPDATE_STATE";

const initialState = {
    fetching:false,
    error:'',
    chriskos:[],
    loadApp:true,
    input:''
}

export default (state = initialState, action) => {
    switch(action.type) {
        case UPDATE_STATE: {
            return Object.assign({}, state, action.value);
        }
        case CHRISKO_CREATED: {
            var newArry = [{
                url: action.json.url,
                shortUrl: api.shortUrl(action.json.id),
                visits: action.json.visits
            }].concat(state.chriskos)
            return Object.assign({}, state, {chriskos:newArry});
        }
        default: {
            return state;
        }
    }
}

export function updateState(value) {
    return {
        type: UPDATE_STATE,
        value
    }
}

function chriskoCreated(json) {
    return {
        type: CHRISKO_CREATED,
        json
    }
}

export function getChrisko(key) {
  return dispatch => {
    return fetch(api.getChrisko(key), { method: 'GET', mode: 'cors',
                redirect: 'follow',
               cache: 'default' })
            .then(response => {
                console.log(response);
              if(response.status !== 200) {
                dispatch(updateState({error:api.shortUrl(key)}));
                dispatch(updateState({loadApp:true}));
              }
              else {
              window.location = response.url;
              }
            })
            .catch(function() {
                console.log('catch error');
                dispatch(updateState({error:`Error grabbing the chrisko`}));
                dispatch(updateState({loadApp:true}));
        });
    };
}

export function createChrisko(url) {
  return dispatch => {
    dispatch(updateState({fetching:true}));
    return fetch(api.createChrisko, { 
        method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
        //mode: 'cors', 
        body: JSON.stringify({url:url}),
               cache: 'default' })
            .then(response => {
              if(response.status !== 200) {
                dispatch(updateState({error:'ERROR'}));
              }
              else {
                return response.json();
              }})
            .then((json) => {
                dispatch(chriskoCreated(json));
                dispatch(updateState({fetching:false}));
            })
            .catch(function() {
                console.log('catch error');
                dispatch(updateState({error:`Error creating the chrisko`}));
                dispatch(updateState({fetching:false}));
        });
  }
}

