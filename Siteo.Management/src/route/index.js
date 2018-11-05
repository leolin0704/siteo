import Router from 'vue-router';
export default new Router({
    routes: [
        {
            path: '/',
            redirect: '/home'
        },
        {
            path: '/login',
            component: resolve => require(['../components/page/login.vue'], resolve)
        },
        {
            path: '/',
            component: resolve => require(['../components/common/layout.vue'], resolve),
            meta: { title: '自述文件' },
            children:[
                {
                    path: '/home',
                    component: resolve => require(['../components/page/home.vue'], resolve),
                    meta: { title: '系统首页' }
                },
                {
                    path: '/about',
                    component: resolve => require(['../components/test.vue'], resolve),
                    meta: { title: '关于我们' }
                },
                {
                    path: '/contact',
                    component: resolve => require(['../components/test2.vue'], resolve),
                    meta: { title: '联系我们' }
                },
                {
                    path: '/banner',
                    component: resolve => require(['../components/page/banner.vue'], resolve),
                    meta: { title: 'banner图' }
                }
            ]
        }
    ]
})
