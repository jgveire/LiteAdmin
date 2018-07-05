<template>
    <div id="app">
        <div class="page-container">
            <md-app>
                <md-app-toolbar class="md-primary">
                    <router-link to="/" class="md-title">LiteAdmin</router-link>
                </md-app-toolbar>

                <md-app-drawer md-permanent="clipped">

                    <md-list>
                        <md-list-item>
                            <md-icon>home</md-icon>
                            <router-link to="/" class="md-list-item-text">Home</router-link>
                        </md-list-item>

                        <md-list-item v-for="table in tables"
                                        :key="table.name">
                            <md-icon>reorder</md-icon>
                            <router-link :to="'/maintain/' + table.name" class="md-list-item-text">{{table.name}}</router-link>
                        </md-list-item>
                    </md-list>
                </md-app-drawer>

                <md-app-content>
                    <router-view />
                </md-app-content>
            </md-app>
        </div>
        <md-snackbar md-position="left" :md-duration="Infinity" :md-active.sync="showSnackbar" md-persistent>
            <span>{{snackbarMessage}}</span>
        </md-snackbar>
    </div>
</template>

<script lang="ts" src="./App.ts"></script>
<style lang="scss" src="./App.scss"></style>
