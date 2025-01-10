import {mount} from '@vue/test-utils';
import {describe, it, expect,} from 'vitest';
import {App} from '/src/App.vue';
import {Users} from '/src/components/Users.vue';
import Investments from '/src/components/Investments.vue';
import Navbar from '/src/components/Navbar.vue';

//Ensure all components that App uses render

describe('App.vue', () => {
    it('renders the SPA, including Users, Navbar, and Investments'), () => {
        const wrapper = mount(App);
        expect(wrapper.findComponent(Users).exists()).toBe(true);
        expect(wrapper.findComponent(Investments).exists()).toBe(true);
        expect(wrapper.findComponent(Navbar).exists()).toBe(true);
    }
});