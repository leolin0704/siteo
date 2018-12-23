import { login, getLoginUser, logout} from '../../api/user';
import { setToken, removeToken } from "../../util/tokenHelper";
// initial state
// shape: [{ id, quantity }]
const state = {
  user: {}
}

// getters
const getters = {
  user: (state, getters, rootState) => {
    return state.user;
  }
}

// actions
const actions = {
  login ({ commit, state }, { account, password }) {
    return login(account, password).then(response => {
      if(response.Status === 1){
        commit("setUser",response.Data.AdminUser)
        setToken(response.Data.Token);
      }

      return response;
    })
  },

  getLoginUser ({ commit, state }) {
    return getLoginUser().then(response => {
      if(response.Status === 1){
        commit("setUser",response.Data.AdminUser);
      }
    });
  },

  logout ({ commit, state }) {
    return logout().then(response => {
      if(response.Status === 1){
        commit("removeUser");
        removeToken();
      }

      return response;
    })
  },
}

// mutations
const mutations = {
  setUser (state, { ID, Account, LastLoginDate, LastLoginIP, Role }) {
    state.user = {
      ID,
      Account,
      LastLoginDate,
      LastLoginIP,
      Role
    };
  },

  removeUser (state) {
    state.user = {}
  }
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
