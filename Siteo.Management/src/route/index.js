import Router from 'vue-router';
export default new Router({
    routes: [
        {
            path: '/',
            redirect: '/home'
        },
        {
            path: '/login',
            component: resolve => require(['../page/login.vue'], resolve)
        },
        {
            path: '/',
            component: resolve => require(['../components/common/layout.vue'], resolve),
            meta: { title: '自述文件' },
            children:[
                {
                    path: '/home',
                    component: resolve => require(['../page/home.vue'], resolve),
                    meta: { title: '系统首页' }
                },
                {
                    path: '/about',
                    component: resolve => require(['../page/about.vue'], resolve),
                    meta: { title: '关于我们' }
                },
                {
                    path: '/contact',
                    component: resolve => require(['../page/contact.vue'], resolve),
                    meta: { title: '联系我们' }
                },
                {
                    path: '/banner',
                    component: resolve => require(['../page/banner.vue'], resolve),
                    meta: { title: 'banner图' }
                }
            ]
        }
    ]
})
