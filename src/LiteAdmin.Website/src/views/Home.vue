<template>
    <div class="home">
        <md-card md-with-hover 
                 v-for="table in tables"
                 @click.native="cardClick" 
                 :data-table-name="table.name">
            <md-card-media-cover md-text-scrim>
                <md-card-media md-ratio="16:9">
                    <img src="../assets/grid.jpg" alt="grid">
                </md-card-media>

                <md-card-area>
                    <md-card-header>
                        <span class="md-title">{{table.name}}</span>
                        <span class="md-subhead">Click here to view the table content of {{table.name}}.</span>
                    </md-card-header>
                </md-card-area>
            </md-card-media-cover>
        </md-card>
    </div>
</template>

<script lang="ts" src="./Home.ts"></script>
<style lang="scss" src="./Home.scss"></style>
